import React from "react";

function ProductList(props) {
  return (
    <tbody>
      {props.products.map(item => (
        <tr key={item.id}>
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
