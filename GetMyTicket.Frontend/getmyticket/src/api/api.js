import axios from "axios";
import Cookies from "js-cookie";

import { toast } from "react-toastify";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.withCredentials = true;

//DISABLE FOR DEVELOPMENT PURPOSES
//axios.interceptors.request.use(function (config) {
//    let isLoggedIn = !!Cookies.get('accessToken');
//
//    if (isLoggedIn) {
//        let accessToken = JSON.parse(Cookies.get('accessToken'));
//        config.headers.Authorization = `${accessToken.tokenType} ${accessToken.accessToken}`;
//    }
//
//    return config;
//}, function (error) {
//    return Promise.reject(error);
//});

axios.interceptors.response.use(function (response) {
    return response.data;
}, function (error) {
    if (!error.response.status === 401) {
        toast.error("Something went wrong. Please, log in to continue.");
        window.location.href = "/";
    } else if (error.code === "ERR_NETWORK") {
        toast.error("Network error. Please, try again.");
    } else if (error.response.status >= 500 && error.response.status <= 599) {
        toast.error("Something went wrong. Please, try again.");
    } else if (error.response.status === 404) {
        window.location.href = "/404";
    } else {
        console.log("Error:", error.response.data);
    }
    return Promise.reject(error);
});

export const api = {
    get: (url) => axios.get(url),
    post: (url, body) => axios.post(url, body),
    put: (url, body) => axios.put(url, body),
    delete: (url) => axios.delete(url)
}