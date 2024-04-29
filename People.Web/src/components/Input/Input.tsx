import React from "react";
import "./Input.css";
import { RegexPatterns } from "../../constants/RegexPatterns";

interface InputProps {
  name: string;
  value: string | number;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  className?: string;
  placeholder?: string;
  required?: boolean;
  type?: "text" | "number" | "email" | "password";
  isPhone?: boolean;
}

const Input: React.FC<InputProps> = ({
  name,
  value,
  onChange,
  className = "",
  placeholder,
  required = false,
  type = "text",
  isPhone = false,
}) => {
  const phonePattern = isPhone ? RegexPatterns.PHONE_NUMBER : undefined;

  return (
    <input
      className={`input ${className}`}
      type={isPhone ? "tel" : type}
      name={name}
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      pattern={phonePattern?.source}
      required={required}
    />
  );
};

export default Input;
