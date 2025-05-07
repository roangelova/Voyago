import { useQuery } from "@tanstack/react-query";
import { api } from "../api/api.js";

export const Booking = {
    bookTrip: (data) => api.post('api/bookings', data),
    cancelBooking: (id) => api.put(`api/bookings/${id}`)
}

//TODO -> FILE STRUCTURE: apis? hooks? how do we handle this 

export function useGetUserBookings(userId){
    const {isPending, error, data: bookings} = useQuery({
        queryKey: ['bookings'],
        queryFn: () => api.get(`api/bookings/${userId}`)
    })

    return {isPending, bookings, error};
}

