import React from "react";

function ProductList(props) {
  return (
    <tbody>
      {props.products.map((item, key) => (
        <tr key={key}>
          <td>{item.nome}</td>
          <td>{item.descricao}</td>
          <td>{item.categoriaNome}</td>
          <td>{item.preco}</td>
        </tr>
      ))}
    </tbody>
  );
}

export default ProductList;
