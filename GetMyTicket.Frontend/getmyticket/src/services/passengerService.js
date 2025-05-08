import { api } from "../api/api.js";

export const Passenger = {
    getPassenger: (userId) => api.get(`api/passengers/by-user/${userId}`),
    createPassenger: (data) => api.post('api/passengers', data),
    editPassenger: (id, data) => api.put(`api/passengers/${id}`, data), 
    getNameAndAge: (bookingId) => api.get(`api/passengers/${bookingId}`)
}