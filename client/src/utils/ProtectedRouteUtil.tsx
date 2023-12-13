import React from "react";
import { Navigate } from "react-router-dom";
import { NavigationPages } from "../constants/NavigationPages";
import { AuthService } from "../services/AuthService";

interface ProtectedRouteProps {
  element: React.ReactElement;
}

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ element }) => {
  const authService = new AuthService();

  return authService.isAuthenticated() ? element : <Navigate to={NavigationPages.Login} />;
};
