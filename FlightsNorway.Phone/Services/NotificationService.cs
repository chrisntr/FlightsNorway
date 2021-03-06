﻿using System;
using System.Diagnostics;
using Microsoft.Phone.Notification;

namespace FlightsNorway.Services
{
    public class NotificationService : IOpenCommunicationChannel
    {
        private const string ChannelName = "FlightsNorwayPushChannel";

        private HttpNotificationChannel _channel;

        public void OpenChannel(Action<string> channelCallback)
        {
            try
            {
                _channel = HttpNotificationChannel.Find(ChannelName);
            }
            catch { }

            if (_channel != null && _channel.ChannelUri != null)
            {
                channelCallback(_channel.ChannelUri.ToString());
            }
            else
            {
                try
                {
                    _channel = new HttpNotificationChannel(ChannelName);
                    _channel.ChannelUriUpdated += (o, e) => channelCallback(e.ChannelUri.ToString());
                    _channel.Open();
                    _channel.BindToShellToast();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }            
        }
    }
}
