import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { MessageService, MessageVM } from 'src/app/services/Message.service';

@Component({
  selector: 'app-messages-list',
  templateUrl: './messages-list.component.html',
  styleUrls: ['./messages-list.component.scss']
})
export class MessagesListComponent implements OnInit {

  constructor(private _messageService: MessageService,
    public _acct: AccountService,) { }

  @Input() messageListToView: MessageVM[] = [];
  @Output() messageClicked = new EventEmitter<MessageVM>();
  @Output() messageReadUnRead = new EventEmitter<{ id: string, isRead: boolean }>();
  @Input() currentMessage;
  ngOnInit(): void {
  }

  openMessage(message: MessageVM, event?: PointerEvent) {
    console.log(`====================== openMessage ${message.Subject}`);
    this.currentMessage = message;
      this.messageClicked.emit(message);
  }

  markAsUnread(message,event){
    event.stopPropagation();
    message.isCurrentUserRead = !message.isCurrentUserRead;
    this.messageReadUnRead.emit(message);
  }
}
