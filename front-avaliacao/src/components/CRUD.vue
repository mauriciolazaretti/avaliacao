<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="itens"
      :options.sync="options"
      :server-items-length="totalItens"
      :loading="loading"
      class="elevation-1"
    >
    <template v-slot:top>
      <v-toolbar flat color="white">
        <v-toolbar-title>Cadastro Pessoas</v-toolbar-title>
        <v-divider
          class="mx-4"
          inset
          vertical
        ></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" fullscreen>
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="primary"
              dark
              class="mb-2"
              v-bind="attrs"
              v-on="on"
            >Nova Pessoa</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.nomeRazaoSocial" label="Nome/RazaoSocial"></v-text-field>
                  </v-col>
                  
                  <v-col cols="12" sm="6" md="4">
                    <v-select :items="tiposPessoa" @change="selectTipoPessoa" label="Tipo Pessoa" v-model="editedItem.tipoPessoa"> 

                    </v-select>
                  </v-col>
                  <v-col cols="12" sm="6" md="4">
                    <v-text-field v-model="editedItem.cpfCnpj"   v-mask="mascara" :placeholder="placeholder" label="CPF/CNPJ">
                    </v-text-field>
                           

                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                   
                   <v-simple-table height="400px">
                      <template v-slot:default>
                        <thead>
                          <tr>
                            <th class="text-left">Tipo Endereço</th>
                            <th class="text-left">Logradouro</th>
                            <th class="text-left">Bairro</th>
                            <th>Ação</th>
                          </tr>
                        </thead>
                        <tbody v-if="editedItem.enderecos.length">
                          <tr  v-for="item in editedItem.enderecos" :key="item.id">
                            <td>
                              {{ labelTipoEndereco(item.tipoEndereco) }}
                            </td>
                            <td>{{ item.logradouro }}</td>
                            <td>{{ item.bairro }}</td>
                            <td><v-icon small @click="deleteEndereco(item)"> mdi-delete</v-icon></td>

                          </tr>
                        </tbody>
                        
                          <tbody v-else>
                            <tr>
                            <td>Sem dados</td>
                            </tr>
                          </tbody>
                      </template>
                    </v-simple-table>

                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                      <v-row>
                          <v-col cols="12" sm="12" md="12">
                            Dados de Endereço
                          </v-col>
                      </v-row>
                      <v-row>
                          <v-col cols="12" sm="3" md="6">
                            <v-select :items="tiposEndereco" label="Tipo Endereço" v-model="enderecoObj.tipoEndereco" >

                            </v-select>
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-text-field v-model="enderecoObj.logradouro" label="logradouro"></v-text-field> 
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-text-field v-model="enderecoObj.numero" label="número"></v-text-field> 
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-text-field v-model="enderecoObj.bairro" label="bairro"></v-text-field> 
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-text-field v-model="enderecoObj.cidade" label="cidade"></v-text-field> 
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-btn @click="salvarEndereco" >Adicionar Endereço</v-btn>
                          </v-col>
                      </v-row>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="12" sm="6" md="4">
                   
                   <v-simple-table height="400px">
                      <template v-slot:default>
                        <thead>
                          <tr>
                            <th class="text-left">Tipo Telefone</th>
                            <th class="text-left">DDD</th>
                            <th class="text-left">Telefone</th>
                            <th> Ação </th>
                          </tr>
                        </thead>
                        <tbody v-if="editedItem.telefones.length">
                          <tr  v-for="item in editedItem.telefones" :key="item.id">
                            <td>
                              {{ labelTipoTelefone(item.tipoTelefone) }}
                            </td>
                            <td>{{ item.ddd }}</td>
                            <td>{{ item.telefone }}</td>
                            <td><v-icon small @click="deleteTelefone(item)"> mdi-delete</v-icon></td>
                          </tr>
                        </tbody>
                        
                          <tbody v-else>
                            <tr>
                            <td>Sem dados</td>
                            </tr>
                          </tbody>
                      </template>
                    </v-simple-table>

                  </v-col>
                  <v-col cols="12" sm="6" md="6">
                      <v-row>
                          <v-col cols="12" sm="12" md="12">
                            Dados de Endereço
                          </v-col>
                      </v-row>
                      <v-row>
                          <v-col cols="12" sm="3" md="6">
                            <v-select :items="tiposTelefone" label="Tipo Telefone" v-model="telefoneObj.tipoTelefone" >

                            </v-select>
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-text-field v-model="telefoneObj.ddd" label="ddd"></v-text-field> 
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-text-field v-model="telefoneObj.telefone" label="telefone"></v-text-field> 
                          </v-col>
                          <v-col cols="12" sm="3" md="3">
                            <v-btn @click="salvarTelefone" >Adicionar Telefone</v-btn>
                          </v-col>
                      </v-row>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
              <v-btn color="blue darken-1" text @click="save">Salvar</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.tipoPessoa="{item}">
      <span v-if="item.tipoPessoa == 0"> Física </span>
      <span v-else> Jurídica </span>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon
        small
        class="mr-2"
        @click="editItem(item)"
      >
        mdi-pencil
      </v-icon>
      <v-icon
        small
        @click="deleteItem(item)"
      >
        mdi-delete
      </v-icon>
    </template>
    <template v-slot:no-data>
      Sem registros
    </template>
    </v-data-table>
  </div>
</template>

<script>

  export default {
    components: {
    },
    data () {

      return {
        mascara: "###.###.###-##",
        tiposPessoa: [
        {text: "Física", value: 0},
        {text: "Juridica", value: 1}
        ],
        tiposEndereco: [
        {text: "Cobrança", value: 0},
        {text: "Comercial", value: 1},
        {text: "Correspondência", value: 2},
        {text: "Entrega", value: 3},
        {text: "Residencial", value: 4},
        ],
        tiposTelefone: [
        {text: "Celular", value: 0},
        {text: "Residencial", value: 1},
        {text: "Comercial", value: 2}
        ],
        dialog: false,
      editedIndex: -1,
      telefoneObjDefault: {
        id: 0,
        ddd: '',
        telefone: '',
        tipoTelefone: '',
        pessoaId: 0
      },
      telefoneObj: {
        id: 0,
        ddd: '',
        telefone: '',
        tipoTelefone: '',
        pessoaId: 0
      },
      tipoEndereco: 0,
      defaultEnderecoObj: {
            id: 0,
            logradouro: '',
            bairro: '',
            numero: '',
            cidade: '',
            tipoEndereco: '',
            pessoaId: 0
          },
      enderecoObj: {
            id: 0,
            logradouro: '',
            bairro: '',
            numero: '',
            cidade: '',
            tipoEndereco: '',
            pessoaId: 0
          },
      editedItem: {
        id: 0,
        cpfCnpj: '',
        nomeRazaoSocial: '',
        tipoPessoa: 0,
        enderecos: [
        ],
        telefones : [
          
        ]
      },
      defaultItem: {
        id: 0,
        cpfCnpj: '',
        nomeRazaoSocial: '',
        tipoPessoa: 0,
        enderecos: [

        ],
        telefones : [
          
        ]
      },
        totalItens: 0,
        itens: [],
        loading: true,
        options: {},
        headers: [
          {
            text: 'ID',
            align: 'start',
            sortable: true,
            value: 'id',
          },
          {text: 'Nome/Razão social', value: 'nomeRazaoSocial'},
          { text: 'tipoPessoa', value: 'tipoPessoa' },
          {text: "CPF/CNPJ", value: 'cpfCnpj'},
          {text: "Ações", value: 'actions'}

        ],
        placeholder: "###.###.###-##"
      }
      
    },
    watch: {
      dialog (val) {
        val || this.close()
      },
      options: {
        handler () {
          this.getDataFromApi()
            .then(json => {
              
              this.itens = json.data.pessoas;
              this.totalItens = json.data.total;
            })
        },
        deep: true,
      }
    },
    computed:{
      formTitle () {
        return this.editedIndex === -1 ? 'Nova pessoa' : 'Editar pessoa'
      },
    },
    mounted () {
      this.getDataFromApi()
        .then(json => {
          console.log(json.data);
          this.itens = json.data.pessoas;
              this.totalItens = json.data.total;
              this.loading = false;
        });
    },
    methods: {
      getDataFromApi () {
      
        return this.getPessoas();
      },
      getPessoas () {
        const { sortBy, sortDesc, page, itemsPerPage } = this.options;
        console.log(this.options);
        let sort = '';
        console.log(sortDesc);
        if(sortDesc[0])
          sort = sortBy[0] + '_desc';
        else
          sort = sortBy[0];
        return this.$axios.get(`https://localhost:44359/pessoa?pagina=${page-1}&qtd=${itemsPerPage}&sort=${sort}`);
      },
       editItem (item) {
        this.editedIndex = this.itens.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },


      close () {
        this.dialog = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },
      salvarEndereco(){
        this.editedItem.enderecos.push(this.enderecoObj);
        this.enderecoObj = Object.assign({}, this.defaultEnderecoObj);
      },
      save () {
        if(this.editedItem.enderecos.length == 0 || this.editedItem.telefones.length == 0){
          alert("você deve cadastrar pelo menos 1 endereço e 1 telefone");
          return;
        }
        this.$axios.post("https://localhost:44359/pessoa", this.editedItem,{ headers: { "Content-Type": "application/json" }}).then((o) => {
          if(o.sucesso){
            if (this.editedIndex > -1) {
              Object.assign(this.itens[this.editedIndex], this.editedItem);
            } else {
              this.itens.push(this.editedItem);
            }


            
          }
          this.getPessoas().then((json) => {
            this.itens = [];
            console.log(json);
            this.itens = json.data.pessoas;
            this.totalItens = json.data.total;
          });
        });
        this.close();
        
        
        
      },
      labelTipoEndereco(codigo){
        if(codigo == 0)
          return 'Cobrança';
        else if(codigo == 1)
          return 'Comercial';
        else if(codigo == 2)
          return 'Correspondência';
        else if(codigo == 3)
        return 'Entrega';
        else
        return 'Residencial';
      },
      labelTipoTelefone(codigo){
        switch(codigo){
          case 0: return 'Celular';
          case 1: return 'Residencial';
          case 2: return 'Comercial';
        }
      },
      salvarTelefone(){
        this.editedItem.telefones.push(this.telefoneObj);
        this.telefoneObj = Object.assign({}, this.telefoneObjDefault);
      },
      selectTipoPessoa(valor){
        console.log(valor);
        if(valor == 1){
          this.mascara = "##.###.###/###-##";
          this.placeholder = '##.###.###/###-##';
        }else{
          this.mascara = "###.###.###-##";
          this.placeholder = '###.###.###-##';
        }
      },
      deleteItem(item){
        var id = item.id;
        this.$axios.get(`https://localhost:44359/pessoa/delete?idPessoa=${id}`).then(() => {
          this.getPessoas().then((json) => {
            this.itens = [];
            console.log(json);
            this.itens = json.data.pessoas;
            this.totalItens = json.data.total;
          });
        });
      },
      deleteTelefone(item){
        let index = this.editedItem.telefones.indexOf(item);
        if(index > -1)
          this.editedItem.telefones.splice(index, 1);
      },
      deleteEndereco(item){
        let index = this.editedItem.enderecos.indexOf(item);
        if(index > -1)
          this.editedItem.enderecos.splice(index, 1);
      }
    },
  }
</script>
