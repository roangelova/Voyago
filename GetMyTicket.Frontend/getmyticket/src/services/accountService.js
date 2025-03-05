import { api } from "../api/api.js";

export const Account = {
    register: (data) => api.post('api/accounts', data),
    login: (data) => api.post('api/authorization/login', data), 
    logout: () => api.post('api/authorization/logout')
}