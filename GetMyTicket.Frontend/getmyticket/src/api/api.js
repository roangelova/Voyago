import axios from "axios";
import Cookies from "js-cookie";

import { toast } from "react-toastify";
import { setAccessAndRefreshTokenInCookies } from "../helpers";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.withCredentials = true;

//ADD JWT TO HEADERS
axios.interceptors.request.use(
  function (config) {
    const accessTokenCookies = Cookies.get("accessToken");
    let isLoggedIn = !!accessTokenCookies;

    if (isLoggedIn) {
      let accessToken = JSON.parse(Cookies.get("accessToken"));
      config.headers.Authorization = `${accessToken.tokenType} ${accessToken.accessToken}`;
    }

    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);

let tokenIsRefreshing = false;

//TODO -> IMPLEMENT A REQ QUEUE

//REFRESH TOKEN SHORTLY BEFORE EXPIRATION
axios.interceptors.request.use(async (config) => {
  if (tokenIsRefreshing) return;

  let accessTokenCookie = Cookies.get("accessToken");
  if (accessTokenCookie) {
    const tokenExpiresIn =
      new Date(JSON.parse(accessTokenCookie).expires) - new Date();
    //refetch if token expires in 60s;
    if (tokenExpiresIn < 120000) {
      await refreshTokens();
    }
  } else {
    return config;
  }

  return config;
});

axios.interceptors.response.use(
  function (response) {
    return response.data;
  },
  function (error) {
    if (!error?.response?.status === 401) {
      toast.error("Something went wrong. Please, log in to continue.");
      window.location.href = "/";
    } else if (error?.code === "ERR_NETWORK") {
      toast.error("Network error. Please, try again.");
    } else if (error?.response?.status >= 500 && error?.response?.status <= 599) {
      throw new Error(error.response.data.detail)
    } else if (error?.response?.status === 404) {
      window.location.href = "/404";
    } else if (error?.status === 400) {
      console.error(error);
    } else {
      console.log("Error:", error?.response?.data);
    }
    return Promise.reject(error);
  }
);

export const api = {
  get: (url) => axios.get(url),
  post: (url, body) => axios.post(url, body),
  put: (url, body) => axios.put(url, body),
  delete: (url) => axios.delete(url),
};

async function refreshTokens() {
  //refresh token func in backend requires refershToken string and userId
  const userId = sessionStorage.getItem("userId");
  const refreshToken = JSON.parse(Cookies.get("refreshToken")).refreshToken;

  const response = await fetch(
    `https://localhost:7243/api/authorization/refreshToken`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ refreshToken, userId }),
    }
  );

  const tokens = await response.json();
  setAccessAndRefreshTokenInCookies(tokens);
  tokenIsRefreshing = false;
}
