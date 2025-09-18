import SimpleNavBar from "../Components";

function MainMenu({ account, onLogout }) {
  let items = [];

  if (!account) {
    items = [
      { label: "Login", href: "/login" },
      { label: "Register", href: "/registration" },
    ];
  } else if (account.role !== "admin") {
    items = [
      { label: "Quizzes", href: "/quizzes" },
      { label: "My Results", href: "/results" },
      { label: "Global Ranking", href: "/globalranking" },
      { label: "Logout", href: "/login", onClick: onLogout },
    ];
  } else if (account.role === "admin") {
    items = [
      { label: "Quizzes", href: "/quizzes" },
      { label: "Create Quiz", href: "/quizzes/create" },
      { label: "Categories", href: "/categories" },
      { label: "Users Results", href: "/usersresults" },
      { label: "My Results", href: "/results" },
      { label: "Global Ranking", href: "/globalranking" },
      { label: "Logout", href: "/login", onClick: onLogout },
    ];
  }

  return <SimpleNavBar links={items} />;
}

export default MainMenu;
