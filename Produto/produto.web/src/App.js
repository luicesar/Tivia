import React, { Component } from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import api from "../src/services/api";
import { Button, Table } from "reactstrap";
import ListHeader from "../src/components/listHeader";
import ProductList from "../src/components/productList";

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      token: "",
      userInfo: {},
      expired: false
    };
  }

  async componentDidMount() {
    this.setState({
      token: "0",
      userInfo: { uid: "0", dataExpiracao: "3 minutos" },
      products: [],
      expired: false
    });
  }

  handleTokenClick = async e => {
    e.preventDefault();

    try {
      const { token, userInfo } = await api.post(`Usuario/auth`);
      await api.setTokenOnLogin();

      userInfo.dataExpiracao = "3 minutos";

      this.setState({
        token: token,
        userInfo: userInfo,
        products: []
      });
    } catch (error) {
    } finally {
      this.setState({ loading: false });
    }
  };

  handleListaClick = async e => {
    e.preventDefault();

    try {
      const { data } = await api.get(`Produto/ListaComCategoria`);
      this.setState({ products: data });
    } catch (error) {
      const { status, statusText } = error.response;
      if (status === 401 && statusText === "Unauthorized") {
        this.setState({
          expired: true,
          userInfo: { uid: "0", dataExpiracao: "Expirou ..." }
        });
      }
    } finally {
      this.setState({ expired: false });
    }
  };

  render() {
    const { products, expired, userInfo } = this.state;

    return (
      <div>
        <section className="section">
          <div className="item token">
            <Button color="success" onClick={this.handleTokenClick}>
              Gerar Token
            </Button>
            <strong>Token: </strong>
            <p>{!expired ? userInfo.uid : "0"}</p>
          </div>
          <div className="item minute">
            {" "}
            <Button color="primary" onClick={this.handleListaClick}>
              Listar products
            </Button>{" "}
            {!expired ? <strong>Expira em: </strong> : "Expirou ..."}
            {!expired ? <p>{userInfo.dataExpiracao}</p> : ""}
          </div>
        </section>

        <div className="container">
          <div className="row">
            <Table>
              <ListHeader />
              <ProductList products={products} />
            </Table>
          </div>
        </div>
      </div>
    );
  }
}

export default App;
