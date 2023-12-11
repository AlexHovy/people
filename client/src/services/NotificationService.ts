import { notificationEmitter } from "../config/EventsConfig";
import { EventTypes } from "../constants/EventTypes";

export class NotificationService {
  static showDefaultNotification(message: string) {
    notificationEmitter.emit(EventTypes.NOTIFICATION_DEFAULT, message);
  }

  static showSuccessNotification(message: string) {
    notificationEmitter.emit(EventTypes.NOTIFICATION_SUCCESS, message);
  }

  static showErrorNotification(message: string) {
    notificationEmitter.emit(EventTypes.NOTIFICATION_ERROR, message);
  }
}
