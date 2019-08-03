import axios from "axios";

class httpClient {
  constructor() {
    const { token } = JSON.parse(localStorage.getItem("@tivia:infouser")) || "";
    this.token = token;
    const instance = axios.create({
      baseURL: "http://localhost:5000/api/",
      headers: { Authorization: `Bearer ${this.token}` }
    });
    this.axiosInstance = instance;
  }

  get(url) {
    return this.axiosInstance
      .get(url)
      .then(resp => {
        return resp;
      })
      .catch(resp => {
        if (resp.response !== undefined && resp.response.status === "401") {
          localStorage.removeItem("@tivia:infouser");
          return resp.response;
        } else {
          return Promise.reject(resp);
        }
      });
  }
  post(url, formData) {
    return this.axiosInstance
      .post(url, formData)
      .then(resp => {
        localStorage.setItem("@tivia:infouser", JSON.stringify(resp.data));
        return resp.data;
      })
      .catch(resp => {
        if (resp.response !== undefined && resp.response.status === "401") {
          localStorage.removeItem("@tivia:infouser");
        } else {
          return Promise.reject(resp);
        }
      });
  }
  setTokenOnLogin = () => {
    const { token } = JSON.parse(localStorage.getItem("@tivia:infouser")) || "";
    this.token = token;

    this.axiosInstance.defaults.headers = {
      Authorization: `Bearer ${this.token}`
    };
  };
  clearTokenOnLogout = () => {
    localStorage.removeItem("@tivia:infouser");
    this.axiosInstance.defaults.headers = {};
  };
}

const instance = new httpClient();

export default instance;
