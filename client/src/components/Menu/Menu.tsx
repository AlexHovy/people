import React, { useState } from "react";
import "./Menu.css";
import { useAuth } from "../../contexts/AuthContext";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars, faTimes } from "@fortawesome/free-solid-svg-icons";
import { AuthService } from "../../services/AuthService";
import { NavigationPages } from "../../constants/NavigationPages";

const Menu: React.FC = () => {
  const authService = new AuthService();

  const { isAuthenticated } = useAuth();
  const [isOpen, setIsOpen] = useState(false);

  const toggleMenu = () => {
    setIsOpen(!isOpen);
  };

  const handleSignOut = async () => {
    await authService.signOut();
  };

  return (
    <nav>
      <div className="menu-icon" onClick={toggleMenu}>
        <FontAwesomeIcon icon={isOpen ? faTimes : faBars} />
      </div>

      {isOpen && (
        <div className="full-screen-menu">
          <ul>
            {!isAuthenticated && (
              <>
                <li>
                  <a href={NavigationPages.Login}>Sign In</a>
                </li>
                <li>
                  <a href={NavigationPages.Home}>Home</a>
                </li>
              </>
            )}
            {isAuthenticated && (
              <>
                <li>
                  <a href={NavigationPages.Dashboard}>Dashboard</a>
                </li>
                <li>
                  <a href={NavigationPages.Category}>Category</a>
                </li>
                <li>
                  <a href={NavigationPages.Transaction}>Transaction</a>
                </li>
                <li>
                  <a href="" onClick={handleSignOut}>
                    Sign Out
                  </a>
                </li>
              </>
            )}
          </ul>
        </div>
      )}
    </nav>
  );
};

export default Menu;
