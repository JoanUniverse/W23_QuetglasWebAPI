using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W20_QuetglasWebAPI.Models
{
    public class MessageModel
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _playerId;

        public string PlayerId
        {
            get { return _playerId; }
            set { _playerId = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private DateTime _messageTime;

        public DateTime MessageTime
        {
            get { return _messageTime; }
            set { _messageTime = value; }
        }
    }
}