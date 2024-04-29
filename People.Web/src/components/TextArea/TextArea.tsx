import React from "react";
import "./TextArea.css";

interface TextAreaProps {
  name: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLTextAreaElement>) => void;
  className?: string;
  placeholder?: string;
}

const TextArea: React.FC<TextAreaProps> = ({
  name,
  value,
  onChange,
  className = "",
  placeholder,
}) => {
  return (
    <textarea
      className={`textarea ${className}`}
      name={name}
      value={value}
      onChange={onChange}
      placeholder={placeholder}
    />
  );
};

export default TextArea;
