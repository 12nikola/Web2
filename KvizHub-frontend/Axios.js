import axios from "axios";

const Axios = axios.create({
  baseURL: "/api",
  withCredentials: true,
});

Axios.interceptors.request.use((requestConfig) => {
  const authToken = localStorage.getItem("auth");

  if (authToken) {
    requestConfig.headers["Authorization"] = `Bearer ${authToken}`;
  }

  return requestConfig;
});

export default Axios;