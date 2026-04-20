import { useEffect, useState } from "react";
import api from "../services/api";

export function useLoans(page = 1, pageSize = 10) {
    const [loans, setLoans] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const fetchLoans = async () => {
        try {
            setLoading(true);

            const response = await api.get("/Loan/List");
            console.log("Respuesta de la API:", response.data);
            setLoans(response.data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchLoans();
    }, []);

    return {
        loans,
        loading,
        error,
        refetch: fetchLoans,
    };
}