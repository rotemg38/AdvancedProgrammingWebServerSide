﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WbeAPIAdvencedProgramming.Controllers
{
    //class to match the requirment of the api
    public class TmpTransfer
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }

    [Route("api/transfer")]
    public class TransferController : Controller
    {
        private readonly UsersService _contextUsers;
        private readonly ChatsService _contextChats;
        private readonly MessagesService _contextMsg;
        private readonly MsgInChatService _contextMsgInChat;

        public TransferController(MessagesService contextMsg, MsgInChatService contextMsgInChat, ChatsService contextChats, UsersService usersService)
        {
            _contextMsg = contextMsg;
            _contextMsgInChat = contextMsgInChat;
            _contextChats = contextChats;
            _contextUsers = usersService;
        }

        // POST api/values
        [HttpPost]
        //todo: check if users should be existing- means if users should have chat?
        public IActionResult Post([FromBody] TmpTransfer info)
        {
            User userTo = _contextUsers.GetUserByUsername(info.To);
            User userFrom = _contextUsers.GetUserByUsername(info.From);
            //if users not existing send error not found
            if (userFrom == null || userTo == null)
            {
                return NotFound();
            }
            Message msg = _contextMsg.AddMsg(info.Content, false);
            Chat chat = _contextChats.GetChatByUsers(info.To, info.From);

            //if this is new messag need to create chat
            if (chat == null)
            {
                chat = _contextChats.AddChat(userTo, userFrom);
            }
            MsgUsers msgUsers = _contextMsgInChat.CreatMsgUsers(userFrom, userTo, msg);
            _contextMsgInChat.AddMsgInChat(chat, msgUsers);
            
            return Created("Post", info);
        }
    }
}
