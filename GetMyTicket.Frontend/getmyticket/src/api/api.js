import axios from "axios";
import Cookies from "js-cookie";

import { setAccessAndRefreshTokenInCookies } from "../helpers";

//used by refreshTokens() to avoid recursive calling
const axiosNoInterceptors = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
  withCredentials: true,
});

axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.withCredentials = true;

let tokenIsRefreshing = false;

//ADD JWT TO HEADERS
axios.interceptors.request.use(
  async function (config) {
    const accessTokenCookie = Cookies.get("accessToken");
    let isLoggedIn = !!accessTokenCookie;
    if (isLoggedIn) {
      try {
        let accessToken = JSON.parse(accessTokenCookie);
        config.headers.Authorization = `${accessToken.tokenType} ${accessToken.accessToken}`;

        //check if token is expiring soon and refresh if needed
        const tokenExpiresIn =
          new Date(JSON.parse(accessTokenCookie).expires) - new Date();
        //refetch if token expires in 20s;
        if (tokenExpiresIn < 20000) {
          tokenIsRefreshing = true;
          await refreshTokens();
        }
      } catch {
        Cookies.remove("accessToken"); //clear cookies if there is an issue
      }
    }

    return config;
  },
  function (error) {
    return Promise.reject(error);
  },
);

//TODO -> IMPLEMENT A REQ QUEUE

axios.interceptors.response.use(function onFulfilled(response) {
    return response.data;
  }, function onRejected(error) {
    return Promise.reject( error.response?.data?.detail ??  "An error occurred.");
  });

export const api = {
  get: (url) => axios.get(url),
  post: (url, body) => axios.post(url, body),
  put: (url, body) => axios.put(url, body),
  delete: (url) => axios.delete(url),
};

async function refreshTokens() {
  const refreshTokenCookie = Cookies.get("refreshToken");
  if (!refreshTokenCookie) return; //TODO -> how do we want to handle this case>
  const refreshToken = JSON.parse(refreshTokenCookie).refreshToken;
  const userId = localStorage.getItem("userId");

  const { data } = await axiosNoInterceptors.post(
    "/api/authorization/refreshToken",
    { refreshToken, userId },
  );

  setAccessAndRefreshTokenInCookies(data);
  tokenIsRefreshing = false;
}
