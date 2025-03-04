import arrowToLeft from '../../assets/icons/arrowToLeft.png';
import arrowToRight from '../../assets/icons/arrowToRight.png';
import { toast } from 'react-toastify';
import { useLocation } from "react-router-dom";
import { useReducer, useEffect } from 'react';
import NavBar from '../common/NavBar';
import CartStepContainer from './CartStepContainer';
import TripDetails from './TripDetails';
import PassengerData from './PassengerData';
import ReviewCart from './ReviewCart';

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
        case 'setTrip':
            return { ...state, trip: action.payload };

        case 'nextStep':
            return state.activeStep >= 2
                ? { ...state, activeStep: 3 }
                : { ...state, activeStep: state.activeStep + 1 };

        case 'previousStep':
            return { ...state, activeStep: Math.max(1, state.activeStep - 1) };

        default:
            toast.error('DEV ONLY: NO ACTION!');
            return state;
    }
}

function Cart() {
    const [state, dispatch] = useReducer(reducer, initialState)

    const location = useLocation();
    const { trip } = location.state || {};

    useEffect(function () {
        if (trip) {
            dispatch({ type: 'setTrip', payload: trip })
        }
    }, trip)

    return (
        <>

            <NavBar />

            <div className="cart">
                <div className="cart__container">
                    <h2>Booking overview</h2>

                    <div className="cart__container-step">
                        <div className={state.activeStep == 1 ? "cart__container-step--activeLeft" : null}>1. Trip details</div>
                        <div className={state.activeStep == 2 ? "cart__container-step--activeMiddle" : null}>2. Passenger data</div>
                        <div className={state.activeStep == 3 ? "cart__container-step--activeRight" : null}>3. Review</div>
                    </div>

                    <CartStepContainer>

                        {state.activeStep === 1 ?
                            <TripDetails trip={state.trip} /> :
                            state.activeStep === 2 ?
                                <PassengerData dispatch={dispatch} /> :
                                state.activeStep === 3 ?
                                    <ReviewCart
                                        trip={state.trip}
                                        dispatch={dispatch} />
                                    : null}

                    </CartStepContainer>

                    <ul className="cart__container-nav">
                        <li onClick={() => dispatch({ type: 'previousStep' })}>
                            <img className="cart__container-nav--icon" src={arrowToLeft} alt='arrow back icon' />
                            <span>BACK</span>
                        </li>
                        <li onClick={() => dispatch({ type: 'nextStep' })}>
                            <img className="cart__container-nav--icon" src={arrowToRight} alt='arrow next icon' />
                            <span>NEXT</span>
                        </li>
                    </ul>
                </div>
            </div>
        </>
    )
}


export default Cart;