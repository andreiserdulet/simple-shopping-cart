import { Product } from "src/app/core/model/product";

export interface Cart {
    id: number;
    products: Product[];
}
