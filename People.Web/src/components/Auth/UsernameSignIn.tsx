import React from "react";
import Button from "../Button/Button";
import Input from "../Input/Input";

interface UsernameSignInProps {
  username: string;
  setUsername: (username: string) => void;
  password: string;
  setPassword: (password: string) => void;
  handleSignIn: () => Promise<void>;
}

const UsernameSignIn: React.FC<UsernameSignInProps> = ({
  username,
  setUsername,
  password,
  setPassword,
  handleSignIn,
}) => {
  const isSignInDisabled = username === "" || password === "";

  return (
    <div className="sign-in-container">
      <Input
        className="sign-in-input-field"
        type="text"
        name="username"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        placeholder="Username"
        required
      />
      <Input
        className="sign-in-input-field"
        type="password"
        name="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="Password"
      />
      <Button
        className="sign-in-button Username"
        onClick={() => handleSignIn()}
        disabled={isSignInDisabled}
      >
        Sign In
      </Button>
    </div>
  );
};

export default UsernameSignIn;
