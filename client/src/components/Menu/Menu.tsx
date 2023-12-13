import React, { useState } from "react";
import "./Menu.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBars, faTimes } from "@fortawesome/free-solid-svg-icons";
import { AuthService } from "../../services/AuthService";
import { NavigationPages } from "../../constants/NavigationPages";
import { useNavigate } from "react-router-dom";

const Menu: React.FC = () => {
  const navigate = useNavigate();
  const authService = new AuthService();

  const [isOpen, setIsOpen] = useState(false);

  const toggleMenu = () => {
    setIsOpen(!isOpen);
  };

  const handleSignOut = async () => {
    await authService.signOut().then(() => {
      navigate(NavigationPages.Home);
    });
  };

  return (
    <nav>
      <div className="menu-icon" onClick={toggleMenu}>
        <FontAwesomeIcon icon={isOpen ? faTimes : faBars} />
      </div>

      {isOpen && (
        <div className="full-screen-menu">
          <ul>
            {!authService.isAuthenticated() && (
              <>
                <li>
                  <a href={NavigationPages.Login}>Sign In</a>
                </li>
                <li>
                  <a href={NavigationPages.Home}>Home</a>
                </li>
              </>
            )}
            {authService.isAuthenticated() && (
              <>
                <li>
                  <a href={NavigationPages.ManagePeople}>Manage People</a>
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
