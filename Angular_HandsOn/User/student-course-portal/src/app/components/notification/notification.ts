import { Component } from '@angular/core';
import { NotificationService } from '../../services/notification';

@Component({
  selector: 'app-notification',
  imports: [],
  // By providing NotificationService here, we create a new, separate instance scoped to this component and its children.
  // It won't share state with other components that provide their own NotificationService instance.
  providers: [NotificationService],
  templateUrl: './notification.html',
  styleUrl: './notification.css',
})
export class Notification {
  constructor(public notificationService: NotificationService) {}
}
