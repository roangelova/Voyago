import axios from "axios";
import Cookies from "js-cookie";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.withCredentials = true;

let isLoggedIn = !!Cookies.get('accessToken');

if (isLoggedIn) {
    let accessToken = JSON.parse(Cookies.get('accessToken'));
    axios.defaults.headers.common['Authorization'] = `${accessToken.tokenType} ${accessToken.accessToken}`;
}

const responseBody = (response) => response.data;


export const api = {
    get: (url) => axios.get(url).then(responseBody),
    post: (url, body) => axios.post(url, body).then(responseBody),
    put: (url, body) => axios.put(url, body).then(responseBody),
    delete: (url) => axios.delete(url).then(responseBody)
}