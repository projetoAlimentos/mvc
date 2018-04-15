
Vue.component('questao-item', {
  props: ['questao'],
  template: `
      <div class="form-group" v-bind:id="questao.identificador">
        <div class="flex-row d-flex">
          <div class="col">
            Dificuldade
          </div>
          <div class="col">
            Descrição
          </div>
          <div class="col">
            Ajuda
          </div>
          <div class="col">
            Opções
          </div>
        </div>
        <div class="flex-row d-flex">
          <div class="col">
            <input class="form-control" id="saaaaa" type="number" v-model="questao.difficulty"/>
          </div>
          <div class="col">
            <input class="form-control" type="text" v-model="questao.description" />
          </div>
          <div class="col">
            <input class="form-control" type="text" v-model="questao.hint"/>
          </div>
          <div class="col">
          <button class="btn btn-default" v-on:click="$emit('apagar')">Apagar</button>
          <button class="btn btn-default" v-on:click="criarOpcao()">Add Opcao</button>
          </div>
        </div>
        <opcao-item v-for="ans in questao.answers" v-bind:answer="ans" :key="ans.identificador"></opcao-item>
        <hr>
      </div>
    `,
    methods: {
      criarOpcao: function() {
        this.questao.answers.push(
          {
            identificador: this.questao.idAnswers++,
            description: '',
            correct: false
          }
        )
      }
    }
})

Vue.component('opcao-item', {
  props: ['answer'],
  data: function () {
    return {
      identificador: 0,
      description: '',
      correct: false
    }
  },
  template: `
    <div class="form-group" v-bind:id="answer.identificador">
      <div class="flex-row d-flex">
        <div class="col">
          Descrição
        </div>
        <div class="col">
          Correta?
        </div>
      </div>
      <div class="flex-row d-flex">
        <div class="col">
          <input class="form-control" type="text" v-model="answer.description" />
        </div>
        <div class="col">
          <input class="" type="checkbox" v-model="answer.correct"/>
        </div>
      </div>
    </div>
    `
})

new Vue(
  {
    el: '#form-questao',
    data: function () {
      return {
        questoes: [],
        id: 0
      }
    },
    created: function () {
      this.fetchData();
    },
    methods: {
      criarQuestao: function() {
        try {
          var topicId = parseInt(window.location.pathname.match(/\/Topic\/([0-9])\/Question/)[1])
          this.questoes.push(
            {
              identificador: this.id,
              difficulty: 0,
              description: '',
              hint: '',
              topicId: topicId,
              idAnswers: 0,
              answers: [],
              batata: [
                {
                  mensagem: "pudim"
                },
                {
                  mensagem: "pudim"
                }
              ]
            
            }
          )  
        } catch (error) {
          alert("Deu muito ruim")
        }
        this.id++
      },
      enviarQuestoes: function() {
        console.log(this.questoes)
        console.log(JSON.stringify(this.questoes))
        fetch('/api/QuestionApi/list', {
          headers: {
            'content-type': 'application/json'
          },
          body: JSON.stringify(this.questoes),
          method: 'POST'
        }).then(data => console.log(data)).catch(err => console.log(err))
        this.questoes
      },
      fetchData: function() {
        fetch('/api/QuestionApi')
          .then((resp) => resp.json())
          .then((data) => this.questoes = data)
          .catch((err) => console.log(err))
      },
      apagar: function(index) {
        var ctz = confirm('Tens certeza disso?');

        if (ctz === true) {
          fetch('/api/QuestionApi/' + this.questoes[index].id, {
            method: 'DELETE'
          }).then(this.questoes.splice(index, 1)).catch(err => console.log(err))
        }
      }

    }
  })
