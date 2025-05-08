import { useLocation } from "react-router-dom";
import { useReducer, useEffect } from 'react';

import arrowToLeft from '../../assets/icons/arrowToLeft.png';
import arrowToRight from '../../assets/icons/arrowToRight.png';

import { toast } from 'react-toastify';

import CartStepContainer from './CartStepContainer';
import TripDetails from './TripDetails';
import PassengerData from './PassengerData';
import ReviewCart from './ReviewCart';

import { Passenger } from "../../services/passengerService";
const userId = sessionStorage.getItem('userId');

const initialState = {
    tripId: null,
    userId: userId,
    passengerId: null,
    passenger: {
        userId: userId,
        firstName: '',
        lastName: '',
        gender: '',
        dob: '2000-01-01',
        documentType: '',
        documentId: '',
        nationality: '',
        documentExpirationDate: '2000-01-01'
    },
    trip: {},
    activeStep: 1
};

function reducer(state, action) {
    switch (action.type) {
        case 'clearCart':
            return { ...state, tripId: null, trip: {} }
        case 'setTrip':
            return { ...state, trip: action.payload, tripId: action.payload.tripId };

        case 'nextStep':
            return state.activeStep >= 2
                ? { ...state, activeStep: 3 }
                : { ...state, activeStep: state.activeStep + 1 };

        case 'previousStep':
            return { ...state, activeStep: Math.max(1, state.activeStep - 1) };
        case "setField":
            return {
                ...state,
                passenger: {
                    ...state.passenger,
                    [action.field]: action.value
                }
            }
        case 'setPassengerId':
            return { ...state, passengerId: action.payload }
        case 'setPassenger':
            return {
                ...state, passenger: {
                    userId: userId,
                    firstName: action.payload.firstName,
                    lastName: action.payload.lastName,
                    gender: action.payload.gender,
                    dob: action.payload.dob,
                    documentType: action.payload.documentType,
                    documentId: action.payload.documentId,
                    nationality: action.payload.nationality,
                    documentExpirationDate: '2000-01-01'
                }
            }
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

        Passenger.getPassenger(state.userId)
            .then(data => {
                if (data) {
                    dispatch({ type: 'setPassengerId', payload: data.passengerId });
                    dispatch({ type: 'setPassenger', payload: data })
                }
            })
            .catch(err => {
                console.error(err)
            })
    }, [])

    return (
        <>
        <title>Cart | Voyago</title>
            <div className="cart">
                <div className="cart__container">
                    <h2>Booking overview</h2>

                    <div className="cart__container-step">
                        <div className={state.activeStep == 1 ? "cart__container-step--activeLeft" : null}>1. Trip details</div>
                        <div className={state.activeStep == 2 ? "cart__container-step--activeMiddle" : null}>2. Passenger data</div>
                        <div className={state.activeStep == 3 ? "cart__container-step--activeRight" : null}>3. Review</div>
                    </div>

                    <CartStepContainer>

                        {state.tripId === null ?

                            <h3>Your card is empty</h3>
                            : (state.activeStep === 1 ?
                                <TripDetails trip={state.trip} /> :
                                state.activeStep === 2 ?
                                    <PassengerData
                                        dispatch={dispatch}
                                        passenger={state.passenger}
                                        passengerId={state.passengerId} /> :
                                    state.activeStep === 3 ?
                                        <ReviewCart
                                            tripId={state.tripId}
                                            userId={state.userId}
                                            passengerId={state.passengerId}
                                            dispatch={dispatch}
                                        />
                                        : null)

                        }

                    </CartStepContainer>

                    <ul className="cart__container-nav">

                        {state.activeStep !== 1 ?
                            <li onClick={() => dispatch({ type: 'previousStep' })}>
                                <img className="cart__container-nav--icon" src={arrowToLeft} alt='arrow back icon' />
                                <span>BACK</span>
                            </li> :
                            <li></li>}

                        {state.activeStep !== 3 ?
                            <li onClick={() => dispatch({ type: 'nextStep' })}>
                                <img className="cart__container-nav--icon" src={arrowToRight} alt='arrow next icon' />
                                <span>NEXT</span>
                            </li> :
                            <li></li>}

                    </ul>
                </div>
            </div>
        </>
    )
}


export default Cart;