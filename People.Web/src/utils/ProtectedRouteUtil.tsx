import React from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { NavigationPages } from "../constants/NavigationPages";
import { AuthService } from "../services/AuthService";

interface ProtectedRouteProps {
  element: React.ReactElement;
}

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ element }) => {
  const navigate = useNavigate();
  const authService = new AuthService();

  const isAuthenticated = authService.isAuthenticated();
  if (!isAuthenticated) navigate(NavigationPages.Login);

  return isAuthenticated ? element : <Navigate to={NavigationPages.Login} />;
};
