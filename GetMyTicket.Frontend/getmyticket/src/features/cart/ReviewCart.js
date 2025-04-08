import { toast } from "react-toastify";
import { Booking } from "../../services/bookingService";
import { useNavigate } from "react-router-dom";

function ReviewCart({
    tripId, userId, dispatch, passengerId
}) {
    const navigate = useNavigate();

    function handleOnClick() {
        if (passengerId === null || passengerId === "") {
            toast.error('Please fill in your passenger details first.')
            return;
        }

        Booking.bookTrip({ tripId, userId , passengerId})
            .then(bookingId => {
                toast.success(`Yuppi! We have reserved your seat! Booking reference: ${bookingId}`);
                navigate('/account/bookings')
                dispatch({ type: 'clearCart' })
            }).catch(error => {
                toast.error(error.response.data.detail);
                //TODO -> what do we want to do next? Option to remove from cart? 
            })
    }
    //TODO -> think of some design: should user be able to qucikly review the booking? ->> THEN DISABLE BUTTON WHILE CREATING BOOKING
    return (
        <div>
            <p>Click below to finish your booking</p>
            <h3 onClick={handleOnClick}>Book</h3>
        </div>
    )
}

export default ReviewCart;