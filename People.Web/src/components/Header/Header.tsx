import Menu from "../Menu/Menu";
import "./Header.css";

export const Header: React.FC = () => {
  return (
    <header className="header">
      <Menu />
    </header>
  );
};

export default Header;
