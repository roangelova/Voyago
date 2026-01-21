import { api } from "../api/api.js";
import Cookies from "js-cookie";

export const Account = {
    register: (data) => api.post('api/accounts', data),
    login: (data) => api.post('api/authorization/login', data), 
    logout: () => api.post('api/authorization/logout'),
    delete: (userId) => api.delete(`api/accounts?userId=${userId}`)
}


  export function cleanUpAfterUserLogout() {
    Cookies.remove("accessToken");
    Cookies.remove("refreshToken");
    localStorage.removeItem("userId");
    window.dispatchEvent(new Event("refreshUser"));
    window.location.href = '/';
  }