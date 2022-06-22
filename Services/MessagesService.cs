﻿
using System;
using System.Collections.Generic;
using Models;
using Repository;

namespace Services
{
    public class MessagesService : IMessagesService
    {
        //private static List<Message> _msgs = new List<Message>();
        //private static int _msgId = 0;

        //private MessageContext _context;
        //private DataContext _dataContext;
        private ServerDbContext _context;

        public MessagesService() {
            //_dataContext = new DataContext();
            //_context = _dataContext.messageContext;
            _context = new ServerDbContext();
        }

        /*public int GenerateMsgId()
        {
            _msgId++;
            return _msgId;
        }*/

        public Message AddMsg(string content, bool sent)
        {
            //var msg = new Message(GenerateMsgId(), content, DateTime.Now.ToString(), sent);
            //_msgs.Add(msg);
            //return msg;
            return _context.insertMsg(new Message(content, DateTime.Now.ToString(), sent));
        }

        public int AddMsg(Message msg)
        {
            //int id = GenerateMsgId();
            //msg.Id = id;
            //_msgs.Add(msg);
            //return id;
            return _context.insertMsg(msg).Id;
        }

       
        /// This function find the message with the given Id
        /// <param Name="id">message Id</param>
        /// <returns>The message if found, otherwise null</returns>
        public Message GetMsgById(int id)
        {
            //return _msgs.Find((msg) => { return msg.Id == id; });
            return _context.getMsg(id);
        }

        /// This function delete a message with the given Id
        /// <param Name="id">message Id</param>
        /// <returns>true for success and false for failur</returns>
        public bool DeleteMsg(int id)
        {
            var msg = GetMsgById(id);
            if (msg != null)
            {
                //_msgs.Remove(msg);
                _context.Remove(msg);
                return true;
            }
            return false;
        }

        /// This function updates the content of the given message
        /// <param Name="content"> the new content</param>
        /// <param Name="idMsg"> the msg we want to update</param>
        /// <returns>true for success and false for failur</returns>
        public bool UpdateMsg(int idMsg, string content)
        {
            Message oldMsg = GetMsgById(idMsg);
            if (oldMsg != null) {
                oldMsg.Content = content;
                _context.updateContent(oldMsg);
                return true;
            }
            return false;
        }

    }
}
