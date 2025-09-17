import { Navigate } from "react-router-dom";

const GuardedRoute = ({ account, pending, roleNeeded, children }) => {
  if (pending) return null;

  if (!account) {
    return <Navigate to="/login" replace />;
  }

  if (roleNeeded && account.role !== roleNeeded) {
    return <Navigate to="/quizzes" replace />;
  }

  return children;
};

export default GuardedRoute;
