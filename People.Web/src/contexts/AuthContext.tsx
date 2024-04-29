import React, { createContext, useState } from "react";
import { AuthService } from "../services/AuthService";

interface AuthContextType {
  isAuthenticated: boolean;
}

interface AuthProviderProps {
  children: React.ReactNode;
}

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const authService = new AuthService();

  const [isAuthenticated] = useState(authService.isAuthenticated());

  return (
    <AuthContext.Provider value={{ isAuthenticated }}>
      {children}
    </AuthContext.Provider>
  );
};
