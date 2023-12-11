import React from "react";
import "./Dialog.css";
import Button from "../Button/Button";

interface DialogProps {
  title: string;
  open: boolean;
  onClose: () => void;
  children: React.ReactNode;
}

const Dialog: React.FC<DialogProps> = ({ title, open, onClose, children }) => {
  if (!open) return null;

  return (
    <div className="dialog-overlay">
      <div className="dialog">
        <h2>{title}</h2>
        <div className="dialog-content">{children}</div>
        <Button className="close" onClick={onClose}>
          Close
        </Button>
      </div>
    </div>
  );
};

export default Dialog;
