import Cookies from "js-cookie";
import React, { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";

function ProtectedRoute() {
    const navigate = useNavigate();
    let isLoggedIn = !!Cookies.get('accessToken');

    useEffect(() => {
        if (!isLoggedIn) {
            navigate('/');
        }
    }, [isLoggedIn, navigate])

    return (
        <>
           <Outlet/>
        </>
    );
}

export default ProtectedRoute;