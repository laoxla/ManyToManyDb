namespace MoviesManyToMany.Controllers {

    export class HomeController {
        public message = 'Hello from the home page!';
    }



    export class AboutController {
        public message = 'Hello from the about page!';
    }

    export class AddActorController {
        public movies;
        public firstName;
        public lastName;
        public selectedMovie;
        
        constructor(private $http: ng.IHttpService) {

            $http.get("/api/movies")
                .then((response) => {
                    this.movies = response.data;
                })
                .catch((response) => {
                })

        }
        addActor() {
            this.$http.post(`/api/movies/${this.selectedMovie.movieId}`, {
                firstName: this.firstName,
                lastName: this.lastName
            })
        }
    }

}
