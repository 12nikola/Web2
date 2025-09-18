import "bootstrap/dist/css/bootstrap.min.css";
import { Route, Routes, Navigate, useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

import MainMenu from "./Components/MainMenu";
import Login from "./Components/Login";
import Registration from "./Components/Registration";
import GuardedRoute from "./Components/GuarderRoute";
import QuizzesList from "./Components/Quiz/QuizzesList";
import SolveQuiz from "./Components/Quiz/SolveQuiz";
import QuizRanking from "./Components/Quiz/QuizRanking";
import UserQuizSolutions from "./Components/Quiz/UserQuizSolutions";
import QuizResultDetail from "./Components/Quiz/QuizResultDetail";
import CategoryManager from "./Components/Quiz/CategoryManager";
import QuizCreator from "./Components/Quiz/QuizCreator";
import QuizUpdate from "./Components/Quiz/AdminComponents/Quiz/QuizUpdate";
import UserResultsPanel from "./Components/Quiz/UserResultsPanel";
import GlobalRanking from "./Components/Quiz/GlobalRanking";

function MainApp() {
  const [currentUser, setCurrentUser] = useState(null);
  const [loadingUser, setLoadingUser] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const storedToken = localStorage.getItem("auth");

    if (storedToken) {
      try {
        const decodedToken = jwtDecode(storedToken);
        const now = Math.floor(Date.now() / 1000);

        if (decodedToken.exp && decodedToken.exp < now) {
          localStorage.removeItem("auth");
          setCurrentUser(null);
          setLoadingUser(false);
          return;
        }

        setCurrentUser({
          token: storedToken,
          role: decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
          username: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        });
      } catch (err) {
        localStorage.removeItem("auth");
      }
    }

    setLoadingUser(false);
  }, []);

  const handleLoginSuccess = (token) => {
    localStorage.setItem("auth", token);
    const decodedToken = jwtDecode(token);

    setCurrentUser({
      token,
      role: decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      username: decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
    });

    navigate("/quizzes");
  };

  const handleUserLogout = () => {
    setCurrentUser(null);
    localStorage.removeItem("auth");
    navigate("/login");
  };

  return (
    <div>
      <MainMenu user={currentUser} handleLogout={handleUserLogout} />

      <Routes>
        <Route
          path="/"
          element={
            currentUser ? <Navigate to="/quizzes" /> : <Navigate to="/login" />
          }
        />
        <Route path="/registration" element={<Registration />} />
        <Route
          path="/login"
          element={<Login handleLogin={handleLoginSuccess} />}
        />

        <Route
          path="/quizzes"
          element={
            <ProtectedRoute loading={loadingUser} user={currentUser}>
              <QuizzesList user={currentUser} />
            </ProtectedRoute>
          }
        />

        <Route
          path="/quizzes/:id/solve"
          element={
            <ProtectedRoute loading={loadingUser} user={currentUser}>
              <SolveQuiz />
            </ProtectedRoute>
          }
        />

        <Route
          path="/quizzes/:id/ranking"
          element={
            <ProtectedRoute loading={loadingUser} user={currentUser}>
              <QuizRanking />
            </ProtectedRoute>
          }
        />

        <Route
          path="/globalranking"
          element={
            <ProtectedRoute loading={loadingUser} user={currentUser}>
              <GlobalRanking />
            </ProtectedRoute>
          }
        />

        <Route
          path="/results"
          element={
            <ProtectedRoute loading={loadingUser} user={currentUser}>
              <UserQuizSolutions username={currentUser?.username} />
            </ProtectedRoute>
          }
        />

        <Route
          path="/results/:id"
          element={
            <ProtectedRoute loading={loadingUser} user={currentUser}>
              <QuizResultDetail />
            </ProtectedRoute>
          }
        />

        <Route
          path="/categories"
          element={
            <ProtectedRoute
              loading={loadingUser}
              user={currentUser}
              requiredRole="admin"
            >
              <CategoryManager />
            </ProtectedRoute>
          }
        />

        <Route
          path="/quizzes/create"
          element={
            <ProtectedRoute
              loading={loadingUser}
              user={currentUser}
              requiredRole="admin"
            >
              <QuizCreator />
            </ProtectedRoute>
          }
        />

        <Route
          path="/quizzes/:id/update"
          element={
            <ProtectedRoute
              loading={loadingUser}
              user={currentUser}
              requiredRole="admin"
            >
              <QuizUpdate />
            </ProtectedRoute>
          }
        />

        <Route
          path="/usersresults"
          element={
            <ProtectedRoute
              loading={loadingUser}
              user={currentUser}
              requiredRole="admin"
            >
              <UserResultsPanel />
            </ProtectedRoute>
          }
        />
      </Routes>
    </div>
  );
}

export default MainApp;
