import React, { useState } from "react";
import "./Auth.css";
import UsernameSignIn from "./UsernameSignIn";
import { AuthService } from "../../services/AuthService";
import { handleError } from "../../utils/ErrorHandlerUtil";

const AuthMethods: React.FC = () => {
  const authService = new AuthService();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSignIn = async () => {
    try {
      authService.signIn(username, password);
    } catch (error: any) {
      handleError(error);
    }
  };

  return (
    <div>
      <UsernameSignIn
        username={username}
        setUsername={setUsername}
        password={password}
        setPassword={setPassword}
        handleSignIn={handleSignIn}
      />
    </div>
  );
};

export default AuthMethods;
