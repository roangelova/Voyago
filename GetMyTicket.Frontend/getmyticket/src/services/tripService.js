import { useMutation } from "@tanstack/react-query";
import { api } from "../api/api.js";

export function useGetTrips(){
    return useMutation({
        mutationFn: (filter) => 
            api.get(`api/trips/search?Type=${filter.type}&StartDate=${filter.startDate}&EndDate=${filter.endDate}&StartCityId=${filter.startCityId}&EndCityId=${filter.endCityId}`)
    })
}