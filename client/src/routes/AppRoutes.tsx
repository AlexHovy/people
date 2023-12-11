import { Route, Routes } from "react-router-dom";
import { NavigationPages } from "../constants/NavigationPages";
import { ProtectedRoute } from "../utils/ProtectedRouteUtil";
import HomePage from "../pages/Home/HomePage";
import LoginPage from "../pages/Login/LoginPage";
import PersonPage from "../pages/People/PersonPage";

const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path={NavigationPages.Home} element={<HomePage />} />
      <Route path={NavigationPages.Login} element={<LoginPage />} />
      <Route
        path={NavigationPages.ManagePeople}
        element={<ProtectedRoute element={<PersonPage />} />}
      />
    </Routes>
  );
};

export default AppRoutes;
