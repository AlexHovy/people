import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import "./App.scss";
import { AuthProvider } from "../../contexts/AuthContext";
import Header from "../Header/Header";
import Footer from "../Footer/Footer";
import AppRoutes from "../../routes/AppRoutes";

const App: React.FC = () => {
  return (
    <AuthProvider>
      <Router>
        <Header />
        <AppRoutes />
        <Footer />
      </Router>
    </AuthProvider>
  );
};

export default App;
