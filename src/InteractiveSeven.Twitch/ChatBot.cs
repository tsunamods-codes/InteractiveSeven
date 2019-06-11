﻿using InteractiveSeven.Core;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.Twitch
{
    public class ChatBot : INotifyPropertyChanged, IChatBot
    {
        private readonly ITwitchClient _client;
        private readonly IList<ITwitchCommand> _commands;
        private readonly IIntervalMessagingService _intervalMessaging;
        private bool isConnected;

        private TwitchSettings Settings => ApplicationSettings.Instance.TwitchSettings;

        public bool IsConnected
        {
            get => isConnected;
            set
            {
                isConnected = value;
                OnPropertyChanged();
            }
        }

        public ChatBot(ITwitchClient twitchClient, IList<ITwitchCommand> commands,
            IIntervalMessagingService intervalMessaging)
        {
            _client = twitchClient;
            _commands = commands;
            _intervalMessaging = intervalMessaging;

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnChatCommandReceived += Client_OnChatCommandReceived;
            _client.OnConnected += Client_OnConnected;
            _client.OnDisconnected += Client_OnDisconnected;
        }

        public void Connect()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(Settings.Username, Settings.AccessToken);
            _client.Initialize(credentials, Settings.Channel);
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.SendMessage(_client.JoinedChannels.FirstOrDefault(), "Disconnecting Interactive Seven!");
            _client.Disconnect();
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            _commands.SingleOrDefault(x => x.ShouldExecute(e.Command.CommandText))
                ?.Execute(CommandData.FromChatCommand(e.Command));
            _intervalMessaging.MessageReceived();
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            IsConnected = true;
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            IsConnected = false;
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            _client.SendMessage(e.Channel, "Interactive Seven is live!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}