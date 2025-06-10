import { useAccountContext } from "./AccountContext"

export default function PassengerList(){

    const data = useAccountContext()
    console.log(data)

    return(
        <h3 >Passengers:</h3>
    )
}