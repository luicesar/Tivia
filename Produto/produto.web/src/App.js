import React, { Component } from "react";
import "./App.css";
import { Button, Table } from "reactstrap";
import "bootstrap/dist/css/bootstrap.css";
import api from "../src/services/api";

class App extends Component {
  state = {
    products: [],
    token: "",
    userInfo: {},
    loading: false
  };

  async componentDidMount() {
    this.setState({
      loading: false,
      token: "0",
      userInfo: { uid: "0", dataExpiracao: 0 },
      products: []
    });
  }

  handleTokenClick = async e => {
    e.preventDefault();

    this.setState({ loading: true });

    try {
      const { token, userInfo } = await api.post(`Usuario/auth`);
      await api.setTokenOnLogin();

      this.setState({
        token: token,
        userInfo: userInfo,
        //        products: [],
        loading: false
      });
    } catch (error) {
    } finally {
      this.setState({ loading: false });
    }
  };

  handleListaClick = async e => {
    e.preventDefault();

    this.setState({ loading: true });

    try {
      const { data } = await api.get(`Produto/ListaComCategoria`);
      this.setState({ products: data });
    } catch (error) {
    } finally {
      this.setState({ loading: false });
    }
  };

  render() {
    const { products, loading, userInfo } = this.state;
    //console.log("renders: ", products);
    return (
      <div>
        <section className="section">
          <div className="item token">
            <Button color="success" onClick={this.handleTokenClick}>
              Gerar Token
            </Button>
            <strong>Token: </strong>
            <p>{userInfo.uid}</p>
          </div>
          <div className="item minute">
            {" "}
            <Button color="primary" onClick={this.handleListaClick}>
              Listar products
            </Button>{" "}
            <strong>Expira em: </strong>
            <p>{userInfo.dataExpiracao}</p>
          </div>
        </section>

        <div className="container">
          <div className="row">
            <Table>
              <thead>
                <tr>
                  <th>Nome</th>
                  <th>Descrição</th>
                  <th>Categoria</th>
                  <th>Preço</th>
                </tr>
              </thead>
              <tbody>
                {products.map(item => (
                  <tr key={item.id}>
                    <td>item.nome</td>
                    <td>item.descricao</td>
                    <td>item.categoriaNome</td>
                    <td>item.preco</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </div>
        </div>
      </div>
    );
  }
}

export default App;
