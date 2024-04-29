import { Route, Routes } from "react-router-dom";
import { NavigationPages } from "../constants/NavigationPages";
import { ProtectedRoute } from "../utils/ProtectedRouteUtil";
import LoginPage from "../pages/Login/LoginPage";
import ManagePersonPage from "../pages/ManagePeople/ManagePeoplePage";
import PeoplePage from "../pages/People/PeoplePage";

const AppRoutes: React.FC = () => {
  return (
    <Routes>
      <Route path={NavigationPages.People} element={<PeoplePage />} />
      <Route path={NavigationPages.Login} element={<LoginPage />} />
      <Route
        path={NavigationPages.ManagePeople}
        element={<ProtectedRoute element={<ManagePersonPage />} />}
      />
    </Routes>
  );
};

export default AppRoutes;
