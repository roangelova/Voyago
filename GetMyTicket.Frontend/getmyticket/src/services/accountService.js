import { api } from "../api/api.js";

export const Account = {
    register: (data) => api.post('api/Accounts', data),
    login: (data) => api.post('api/Authorization/login', data), 
    logout: () => api.post('api/Authorization/logout')
}