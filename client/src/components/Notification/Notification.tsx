import React, { useState, useEffect } from "react";
import "./Notification.css";
import { NotificationTypes } from "../../constants/NotificationTypes";

interface NotificationProps {
  message: string;
  type?: NotificationTypes;
  isVisible: boolean;
}

const Notification: React.FC<NotificationProps> = ({
  message,
  type = NotificationTypes.DEFAULT,
  isVisible,
}) => {
  if (!isVisible) return null;

  return <div className={`notification ${type}`}>{message}</div>;
};

export default Notification;
