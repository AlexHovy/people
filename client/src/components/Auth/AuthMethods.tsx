import React, { useState } from "react";
import "./Auth.css";
import UsernameSignIn from "./UsernameSignIn";
import { AuthService } from "../../services/AuthService";
import { handleError } from "../../utils/ErrorHandlerUtil";
import { LoginDto } from "../../dtos/LoginDto";
import { NavigationPages } from "../../constants/NavigationPages";
import { useNavigate } from "react-router-dom";

const AuthMethods: React.FC = () => {
  const navigate = useNavigate();
  const authService = new AuthService();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSignIn = async () => {
    try {
      const loginDto: LoginDto = {
        username: username,
        password: password,
      };
      await authService.signIn(loginDto).then(() => {
        navigate(NavigationPages.ManagePeople);
      });
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
