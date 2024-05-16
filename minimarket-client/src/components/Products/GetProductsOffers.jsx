import Api from "../../Api";

const GetProductsOffers = async () => {
    const api = Api();
    const response = await api.get("/api/products/offers");

    return response.data;
};

export default GetProductsOffers;