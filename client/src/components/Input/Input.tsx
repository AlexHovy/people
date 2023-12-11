import React from "react";
import "./Input.css";

interface InputProps {
  name: string;
  value: string | number;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  className?: string;
  placeholder?: string;
  required?: boolean;
  type?: "text" | "number" | "email" | "password";
}

const Input: React.FC<InputProps> = ({
  name,
  value,
  onChange,
  className = "",
  placeholder,
  required = false,
  type = "text",
}) => {
  return (
    <input
      className={`input ${className}`}
      type={type}
      name={name}
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      required={required}
    />
  );
};

export default Input;
