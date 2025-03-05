import { api } from "../api/api.js";

export const Booking = {
    bookTrip: (data) => api.post('api/Booking', data),
}

