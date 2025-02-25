import arrowToLeft from '../../assets/icons/arrowToLeft.png';
import arrowToRight from '../../assets/icons/arrowToRight.png';
import { toast } from 'react-toastify';
import { useLocation } from "react-router-dom";
import { useReducer } from 'react';
import NavBar from '../common/NavBar';
import CartStepContainer from './CartStepContainer';

//trip obj
//    "tripId": "9d9bf04e-14f0-4fb9-b3b2-08dd4cdbf0e8",
//    "startTime": "2025-05-18T18:00:00",
//    "endTime": "2025-05-18T20:20:00",
//    "price": 220,
//    "endCityName": "Varna",
//    "startCityName": "Munich",
//    "transportationProviderName": "TransAvia"

const initialState = {
    tripId: null,
    userId: null,
    passenderId: null,
    trip: {},
    activeStep: 1
};

function reducer(state, action) {
    switch (action.type) {

        default:
            toast.error('DEV ONLY: NO ACTION!');
    }
}

function Cart() {
    const [state, dispatch] = useReducer(reducer, initialState)

    const location = useLocation();
    const { trip } = location.state || {};
    console.log(trip)


    //TODO -> fix header style so that it is fully reusable. right now,
    //the nabvar has the hight of the header;
    //+ ADD HOME NAV TO NAVBAR
    return (
        <>

            <NavBar />

            <div className="cart">
                <div className="cart__container">
                    <h2>Booking overview</h2>


                    <div className="cart__container-step">
                        <div className="cart__container-step--active">1. Trip details</div>
                        <div>2. Passenger data</div>
                        <div>3. Review</div>
                    </div>

                    <CartStepContainer>
                        <p>render the step component conditionally here</p>
                    </CartStepContainer>

                    <div className="cart__container-nav">
                        <a href='#'>
                            <li>
                                <img className="cart__container-nav--icon" src={arrowToLeft} alt='arrow back icon' />
                                <span>BACK</span>
                            </li>
                        </a>
                        <a href='#'>
                            <li >
                                <img className="cart__container-nav--icon" src={arrowToRight} alt='arrow next icon' />
                                <span>NEXT</span>
                            </li>
                        </a>
                    </div>
                </div>
            </div>
        </>
    )
}


export default Cart;